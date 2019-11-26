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
        sh '''docker ps -q --filter "name=freeapitest" | grep -q . && docker stop freeapitest && docker rm -fv freeapitest
docker run -d -p 5000:80 --env ConnectionString=$connectionString --env DatabaseName=$DatabaseName --env JobCollectionName=$JobCollectionName --env JwtIssuer=$JwtIssuer --env JwtKey=$JwtKey --name freeapitest apitest:latest


'''
      }
    }

    stage('Done') {
      steps {
        echo 'Done'
      }
    }

  }
}