pipeline {
  agent any
  stages {
    stage('Test') {
      steps {
        sh '''
  dotnet test '''
        warnError(message: 'Tests failed', catchInterruptions: true) {
          emailext(subject: 'Tests failed', body: 'The tests failed', from: 'Jenkins', to: 'emilvinkel@gmail.com', attachLog: true)
        }

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
        emailext(subject: 'Build completed', body: 'Jenkins Build completed', attachLog: true, to: 'emilvinkel@gmail.com')
      }
    }

  }
}