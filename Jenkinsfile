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
  environment {
    ConnectionString = 'mongodb+srv://EmilVinkel:CS26WX0gSk1ox3xx@cluster0-bmthv.mongodb.net/Freelance-database?retryWrites=true&w=majority'
    DatabaseName = 'Freelance-database'
    JwtIssuer = 'FreelanceApi'
    JwtKey = 'y0fqoJPTsE4eeKSf'
    StudentCollectionName = 'Student'
    JobCollectionName = 'Jobs'
  }
}