pipeline {
    agent any

    stages {
        stage('Stage 1') {
            steps {
                echo 'Entering Stage 1'
                bat 'mkdir "C:\\kayas"'
            }
        }
        stage('Stage 2') {
            steps {
                echo 'Entering Stage 2'
                git branch: 'beta', credentialsId: '4e25df4d-b425-4e13-8e61-a6a66615c782', url: 'https://github.com/megassoft/CICD-Test.git'
            }
        }
    }
}
