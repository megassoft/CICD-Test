//Install the ".NET SDK Support" Plugin to build the projects
//Install the "Docker Pipeline" Plugin to build the projects
//Install the "SSH Agent" Plugin to build the projects

def outerVariable = 'test'

pipeline {

    environment{
        DOCKER_CRE = 'docker_chelsea98fedu'
        registry = "chelsea98fedu/dockertest"
        dockerimage = ''
    }
    
    agent any
    
    stages {
        stage('Starting') {
            steps {
                script {
                    
                    try {
                        if (DOCKER_CRE == "docker_chelsea98fedu") {
                            echo "Key is $DOCKER_CRE"
                        } else {
                            echo 'Key is NA'
                            bat 'exit 1'
                            //currentBuild.result = 'ABORTED'
                        } 
                    } catch(e) {
                        currentBuild.result = 'ABORTED'
                    }

                }

                //node('node 1') {
                    
                    
                //}
                
                echo "Key3 is ${outerVariable}"
                
                echo "Key2 is ${env.WORKSPACE} / $JENKINS_URL"
            }
        }
        stage('Parallel In Sequential') {
            //you can force your parallel stages to all be aborted when any one of them fails
            failFast true
            
            parallel {
                stage('Branch A') {
                    steps {
                        echo "In Parallel 1"
                    }
                }
                stage('Branch B') {
                    steps {
                        echo "In Parallel 2"
                    }
                }
            }
        }
        stage('Building the Project') {
            when { 
                expression { return (currentBuild.result == 'SUCCESS' || currentBuild.result == null) }
            }
            //when {
            //    branch 'master'
            //    environment name: 'DOCKER_CRE', value: 'docker_chelsea98fedu'
            //}
            steps {
                echo 'Entering Building the Project'
                git branch: 'beta', credentialsId: '4e25df4d-b425-4e13-8e61-a6a66615c782', url: 'https://github.com/megassoft/CICD-Test.git'
                dotnetBuild configuration: 'Release', project: 'CICD Test.sln', sdk: '.NET 5.0'
            }
        }
        stage('Testing the Project') {
            when { 
                expression { return (currentBuild.result == 'SUCCESS' || currentBuild.result == null) }
            }
            steps {
                echo 'Entering Testing the Project'
                dotnetTest configuration: 'Release', noBuild: true, noRestore: true, logger: 'trx;LogFileName="TestOutput.xml"', project: 'CICD Unit Test/CICD Unit Test.csproj', sdk: '.NET 5.0'
            }
            post {
                always {
                    echo "result is ${currentBuild.result}"
                }
                success {
                    echo 'success'
                }
                unstable {
                    echo 'unstable'
                    //error('Quitting')
                    //bat 'exit 1'
                }
                failure {
                    echo 'failure'
                                            
                    emailext subject: '$DEFAULT_SUBJECT',
                        //subject: '$PROJECT_NAME - Build # $BUILD_NUMBER - ERROR!',
                        recipientProviders: [[$class: 'DevelopersRecipientProvider'], [$class: 'RequesterRecipientProvider']], 
                        replyTo: '$DEFAULT_REPLYTO',
                        to: '$DEFAULT_RECIPIENTS',
                        body: '$DEFAULT_CONTENT'
                        //body: 'Check console output at $BUILD_URL to view the results on $PROJECT_NAME'
                        
                    //error('Quitting')
                    //bat 'exit 1'
                }
                changed {
                    echo 'changed'
                }
            }
        }
        stage('Building the Image') {
            when { 
                expression { return (currentBuild.result == 'SUCCESS' || currentBuild.result == null) }
            }
            steps {
                echo 'Entering Building the Image'
            //    script {
            //        def errorStr = "Exiting Building the Image"
                    
            //        if (currentBuild.result == 'SUCCESS' || currentBuild.result == null) {
            //            echo errorStr
            //            error(errorStr)
            //            //you could use below code instead of error()
            //            //bat 'exit 1'
            //        }
            //    }
                script {
                    dockerimage = docker.build registry + ":$BUILD_NUMBER"
                }
                //bat 'docker build -t chelsea98fedu/dockertest .'
            }
        }
        stage('Deploying the Image') {
            when { 
                expression { return (currentBuild.result == 'SUCCESS' || currentBuild.result == null) }
            }
            steps {
                echo 'Entering Deploying the Image'
                script {
                    docker.withRegistry('', DOCKER_CRE) {
                        dockerimage.push()
                    }
                }
            }
        }
        stage('Running the Image') {
            when { 
                expression { return (currentBuild.result == 'SUCCESS' || currentBuild.result == null) }
            }
            steps {
                echo 'Entering Running the Image'
                
                withCredentials([sshUserPrivateKey(credentialsId: 'dev-server', keyFileVariable: 'identity', passphraseVariable: '', usernameVariable: 'chelsea98')]) {
                    script {
                      def remote = [:]
                      remote.name = 'worker1'
                      remote.host = '10.0.2.101'
                      remote.allowAnyHosts = true
                      remote.user = 'chelsea98'
                      remote.identityFile = identity
                      
                      stage('Remote SSH') {
                        
                        // add the user (chelsea98) to sudoers file on Linux Server ( <username> ALL=(ALL) NOPASSWD:ALL ) 
                        sshCommand remote: remote, command: "sudo docker run -d -p 5001:5001 --name kaya ${registry}:${BUILD_NUMBER}"
                        
                        //// if there is no sudoers
                        //def password = input message: 'Please enter the password',
                        //                 parameters: [password(defaultValue: '',
                        //                              description: '',
                        //                              name: 'Password')]
                        
                        //sshCommand remote: remote, command: "echo ${password} | sudo -S docker run -d -p 5001:5001 --name kaya ${registry}:224"
                        
                        
                        //writeFile file: 'abc.sh', text: 'ls'
                        //sshPut remote: remote, from: 'abc.sh', into: '.'
                        //sshGet remote: remote, from: 'abc.sh', into: 'bac.sh', override: true
                        //sshScript remote: remote, script: 'abc.sh'
                        //sshRemove remote: remote, path: 'abc.sh'
                      } 
                    }
                }
                
                //bat "docker run -d -p 5001:5001 --name kaya ${registry}:${BUILD_NUMBER}"
            }
        }
        stage('Cleaning the Image') {
            when { 
                expression { return (currentBuild.result == 'SUCCESS' || currentBuild.result == null) }
            }
            steps {
                withCredentials([sshUserPrivateKey(credentialsId: 'dev-server', keyFileVariable: 'identity', passphraseVariable: '', usernameVariable: 'chelsea98')]) {
                    script {
                      def remote = [:]
                      remote.name = 'worker1'
                      remote.host = '10.0.2.101'
                      remote.allowAnyHosts = true
                      remote.user = 'chelsea98'
                      remote.identityFile = identity
                      
                      stage('Remote SSH') {
                        
                        // add the user (chelsea98) to sudoers file on Linux Server ( <username> ALL=(ALL) NOPASSWD:ALL ) 
                        echo 'Entering Cleaning the Image'
                        echo 'Waiting for test'
                        sleep 30
                        sshCommand remote: remote, command: "docker rm kaya -f"
                        sleep 10
                        sshCommand remote: remote, command: "docker rmi $registry:$BUILD_NUMBER"
                      } 
                    }
                }
            }
        }
    }
    post {
        always {
            //junit '**/TestResults/*.xml'
            //junit 'CICD Unit Test/TestResults/TestOutput.xml'
            echo 'The Pipeline ended!'
        }
        failure {
            echo 'The Pipeline failed!'
            //bat 'mkdir "C:\\kayas"'
        }
    }
}       
