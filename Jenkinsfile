pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        sh '''cd Backend
docker build --tag apitest:latest . '''
      }
    }

    stage('Deploy') {
      steps {
        sh '''if [ ! "$(docker ps -q -f name=freeapitest)" ]; then
    if [ "$(docker ps -aq -f status=exited -f name=freeapitest)" ]; then
        # cleanup
        docker rm freeapitest
    fi
    docker run -d -p 5000:80 --env ConnectionString=$ConnectionString --env DatabaseName=$DatabaseName --env JobCollectionName=$JobCollectionName --env JwtIssuer=$JwtIssuer --env JwtKey=$JwtKey --name freeapitest apitest:latest
fi

'''
      }
    }

  }
}