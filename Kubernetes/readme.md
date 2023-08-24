## Introduce

| File | Type                  | Description        | Output |
| --- |-----------------------|--------------------|--------------------|
| environment.yaml | configMap             | 共用變數             | env |
| postgres-pv.yaml | PersistentVolume      | pv 設定檔             | postgres-pv |
| postgres-pvc.yaml | PersistentVolumeClaim | pvc 設定檔            | postgres-pvc |
| postgres-db.yaml | Deployment & Service  | postgres & adminer | postgres-db-service & adminer-service |
| shopping-api.yaml | Deployment & Service  | shopping-api       | shopping-api-service |
| shopping-client.yaml | Deployment & Service  | shopping-client    | shopping-client-service |

#### postgres
兩個變數需要設定 **使用者帳號**與**密碼**
- `POSTGRES_USER`
- `POSTGRES_PASSWORD`

#### shopping-client 的 API URL 設定
http://{serviceName}.{namespace}:{port}
> http://**shopping-api-service.default**:5001

#### shopping-api DB 的連線字串
database, username 與 password 與 postgres-db.yaml 的環境變數相同 \
要注意的是 host, {serviceName}.{namespace} \
也就是 `postgres-db-service.default`
> host=**postgres-db-service.default**;database=shopping;username=postgres;password=postgres;

## Run
### Push image to Docker Hub
- `docker build -t billyao78/shopping-api -f shopping.api/Dockerfile .`
- `docker push billyao78/shopping-api`
- `docker build -t billyao78/shopping-client -f shopping.client/Dockerfile .`
- `docker push billyao78/shopping-client`
### apply yaml files
`kubectl apply -f .\Kubernetes\`

### minikube service
`kubectl service shopping-client-service`

### delete service and deployment
`kubectl delete -f .\Kubernetes\`