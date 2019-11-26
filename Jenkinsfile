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
        sh '''docker stop rabbitmq || true && docker rm rabbitmq || true;
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