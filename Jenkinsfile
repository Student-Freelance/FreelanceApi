pipeline {
  agent any
  stages {
    stage('Test') {
      steps {
        sh '''
  dotnet test '''
      }
    }

    stage('Build') {
      steps {
        sh '''cd Backend
docker build --tag apitest:latest . '''
      }
    }

    stage('Deploy') {
      steps {
        sh '''docker stop freeapitest || true && docker rm freeapitest || true;
docker run -d -p 5000:80 --env ConnectionString=$ConnectionString --env DatabaseName=$DatabaseName --env JobCollectionName=$JobCollectionName --env JwtIssuer=$JwtIssuer --env JwtKey=$JwtKey --name freeapitest apitest:latest


'''
      }
    }

    stage('Finished') {
      steps {
        mail(to: 'emilvinkel@gmail.com', replyTo: 'noreply@gmail.com', subject: 'Deploymenthas completed', body: 'Deploying backend has completed')
      }
    }

  }
}