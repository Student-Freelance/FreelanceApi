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
        sh '''docker run -d -p 5000:80 --name freeapitest apitest:latest

'''
      }
    }

  }
}