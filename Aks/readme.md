### **Install the Azure CLI**

https://docs.microsoft.com/en-us/cli/azure/install-azure-cli
https://docs.microsoft.com/en-us/cli/azure/install-azure-cli-windows?tabs=azure-cli
### az login
- Create a resource group \
  `az group create -n RG-Aks-Demo --location southeastasia` \
  `az account list-locations --output table` 可以透過此指令查看 location list
- Create an Azure Container Registry \
  `az acr create -g RG-Aks-Demo -n crshopping --sku Basic`
- Enable Admin Account for ACR pull \
  `az acr update -n crshopping --admin-enabled true`
- Login to the container registry \
  `az acr login -n crshopping`

### Push image to ACR
- get the login server address \
  `az acr list -g RG-aks-Demo --query "[].{acrLoginServer:loginServer}" --output table`
  > **crshopping.azurecr.io**
- Tag images \
  `docker tag image shopping-api:latest crshopping.azurecr.io/shopping-api:v1` \
  `docker tag image shopping-client:latest crshopping.azurecr.io/shopping-client:v1`
- Check images \
  `docker images`
- Push images to ACR \
  `docker push crshopping.azurecr.io/shopping-api:v1` \
  `docker push crshopping.azurecr.io/shopping-client:v1`
- List image in registry \
  `az acr repository list -n crshopping --output table`
### Create Image pull secret
```
kubectl create secret docker-registry acr-secret \
--docker-server=crshopping.azurecr.io \
--docker-username=crshopping \
--docker-password=${password} \
--docker-email=yao780913@gmail.com
```

- `kubectl get secret`
    ```
    NAME           TYPE                             DATA   AGE
    acr-secret     kubernetes.io/dockerconfigjson   1      14s
    mongo-secret   Opaque                           2      20h
    ```
- 同時要修改 .yaml 檔案，設定 `imagePullSecrets`

### Create AKS cluster with ACR
- Create AKS cluster \
    `az aks create --name myAksCluster --resource-group RG-Aks-Demo --node-count 1 --location southeastasia --tier free`
- Install the Kubernetes CLI \
  `az aks install-cli`
- Connect to the cluster using kubectl \
  `az aks get-credentials -g RG-Aks-Demo -n myAksCluster`
    - To verify \
      `kubectl get nodes` \
      `kubectl get all`
      - Check kube config \
        `kubectl config get-contexts` \
        ```
        CURRENT   NAME             CLUSTER          AUTHINFO                               NAMESPACE
                  docker-desktop   docker-desktop   docker-desktop
        *         myAksCluster     myAksCluster     clusterUser_RG-Aks-Demo_myAksCluster
        ```
        切換 context  `kubectl config current-context xxx`

### Delete Resources
- Delete AKS cluster \
  `az aks delete --name myAksCluster --resource-group RG-Aks-Demo`
- Delete ACR \
  `az acr delete -n crshopping -g RG-Aks-Demo`
- Set context \
  `kubectl config current-context docker-desktop`
- Delete context \
  `kubectl config delete-context myAksCluster`