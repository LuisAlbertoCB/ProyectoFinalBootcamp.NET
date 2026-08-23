# CleanArchitecture.Full — Accounts API

API .NET para gestionar Clientes y Cuentas. Usa PostgreSQL, loguea con Serilog + Seq, se despliega en Kubernetes con Helm, y tiene CI/CD en GitHub Actions.

## Cómo levantarlo (rápido, con Docker Compose)

```bash
docker compose up --build
```

Con eso arriba tenés:
- API en `http://localhost:8081`
- Postgres en `localhost:5432` (usuario/clave `postgres`/`postgres`)
- Seq en `http://localhost:5341`

## Cómo levantarlo en Kubernetes (Minikube)

Necesitás Minikube, kubectl y Helm instalados.

```powershell
minikube start

# Namespace + Postgres + Seq
kubectl apply -f k8s/00-namespace.yaml
kubectl apply -f k8s

# Cargar el esquema y los datos de ejemplo
Get-Content database/02_create_table_and_seed.sql -Raw |
  kubectl exec --stdin statefulset/postgres --namespace accounts -- `
    psql -v ON_ERROR_STOP=1 -U postgres -d accountsdb

# Desplegar la API con Helm
helm upgrade --install accounts-api ./helm/accounts-api `
  --namespace accounts --create-namespace `
  -f helm/accounts-api/values.yaml `
  -f helm/accounts-api/values-prod.yaml `
  --wait --timeout 3m
```

La API queda expuesta en `http://<minikube ip>:30080` (`minikube ip` te da la IP).

Para desplegar a otro entorno, cambiá el archivo de values:

```powershell
helm upgrade --install accounts-api ./helm/accounts-api `
  --namespace accounts --create-namespace `
  -f helm/accounts-api/values.yaml -f helm/accounts-api/values-dev.yaml `
  --wait
```

(`values-dev.yaml`, `values-qa.yaml` o `values-prod.yaml`, según a dónde quieras desplegar.)

## Migraciones de EF Core

```bash
dotnet ef database update `
  --project src/CleanArchitecture.Full.Infrastructure `
  --startup-project src/CleanArchitecture.Full.Api
```

## Configuración

La app lee todo de variables de entorno. Lo no sensible viene de un `ConfigMap`; la connection string de Postgres viene de un `Secret`. Ambos se inyectan al contenedor con `envFrom` (ver [templates/deployment.yaml](helm/accounts-api/templates/deployment.yaml)).

## Logging

Serilog manda logs a la consola y a Seq (HTTP), con propiedades estructuradas como `RequestId` y `MachineName` (el pod que lo generó) en cada request. Con varias réplicas corriendo, podés buscar en Seq (`http://<minikube ip>:30341`) por esas propiedades para correlacionar eventos entre pods, por ejemplo:

```
Application = 'CleanArchitecture.Full.Api' and @Level = 'Error'
```

Salida real de esa búsqueda, mostrando eventos de distintos Pods: [docs/capturas/seq-busqueda.txt](docs/capturas/seq-busqueda.txt).

## Aautorecuperación en acción

```powershell
kubectl get pods -n accounts -l app.kubernetes.io/name=accounts-api
kubectl delete pod <nombre-de-un-pod> -n accounts
kubectl get pods -n accounts -l app.kubernetes.io/name=accounts-api -w
```

El ReplicaSet recrea el Pod borrado, para mantener las réplicas declaradas. Salida real: [docs/capturas/self-healing.txt](docs/capturas/self-healing.txt).

## Escalar

```powershell
helm upgrade accounts-api ./helm/accounts-api `
  --namespace accounts `
  -f helm/accounts-api/values.yaml -f helm/accounts-api/values-prod.yaml `
  --set replicaCount=4 `
  --wait
```

Salida real de un escalado a 4 réplicas: [docs/capturas/escalado.txt](docs/capturas/escalado.txt).

## CI/CD

- [ci.yml](.github/workflows/ci.yml): corre en cada push y PR a `main` — build, tests y validación del chart de Helm.
- [cd.yml](.github/workflows/cd.yml): se dispara solo si el CI pasó en `main`. Construye y publica la imagen a Docker Hub, y despliega en el Minikube local con Helm.

Los secretos (`DOCKERHUB_USERNAME`, `DOCKERHUB_TOKEN`) viven en GitHub Secrets, nunca en el código.
