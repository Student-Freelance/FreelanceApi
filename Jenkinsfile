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
        sh '''CONTAINER=$freeapitest
 
RUNNING=$(docker inspect --format="{{ .State.Running }}" $freeapitest 2> /dev/null)

if [ $? -eq 1 ]; then
  echo "\'$freeapitest \' does not exist."
else
  /usr/bin/docker rm --force $freeapitest 
fi
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