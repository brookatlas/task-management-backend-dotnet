.DEFAULT_GOAL := up
.PHONY: build run clean stop init-db

clean: stop
	docker image rm -f task-management-backend-dotnet

build:
	docker build ./ --tag task-management-backend-dotnet:latest

init-db:
	timeout 2
	docker cp initMongo.js task-management-db:/
	docker cp initMongo.sh task-management-db:/
	docker exec -it task-management-db bash initMongo.sh

run:
	docker-compose -f docker-compose.yml up -d --force-recreate

stop:
	docker-compose -f docker-compose.yml down

up: build run init-db
	










