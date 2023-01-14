#!/bin/bash

#apt-get update && apt-get install -y ansible

echo "* Add hosts ..."
echo "192.168.100.100 website docker" >> /etc/hosts

echo "* Add any prerequisites ..."
apt-get update
apt-get install -y ca-certificates curl gnupg lsb-release

echo "* Add Docker repository and key ..."
mkdir -p /etc/apt/keyrings
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg
echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu \
$(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null

echo "* Install Docker ..."
apt-get update
apt-get install -y docker-ce docker-ce-cli containerd.io docker-compose docker-compose-plugin

echo "* Add vagrant user to docker group ..."
usermod -aG docker vagrant

echo "* Creating docker-compose file ..."
echo "version: '3.4'

networks:
  services-network:
    driver: bridge 

services:
  webservice:
    container_name: webservice
    image: webservice:latest
    depends_on:
      - "apiservice"
    build:
      context: webservice
      dockerfile: Dockerfile
    environment:
        - API_SERVICE=http://apiservice:80/
    ports:
      - "8080:80"
    networks:
      - services-network
  
  apiservice:
    container_name: apiservice
    image: apiservice:latest
    build:
      context: apiservice
      dockerfile: Dockerfile    
    networks:
      - services-network" >> website/docker-compose.yml
	  
echo "* Starting docker-compose ..."
cd website
docker-compose build
docker-compose up