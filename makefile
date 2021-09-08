.DEFAULT_GOAL := up
.PHONY: build run clean stop

clean: stop
	docker image rm -f task-management-backend-dotnet

build:
	docker build ./ --tag task-management-backend-dotnet:latest --no-cache

run:
	docker-compose -f docker-compose.yml up -d --force-recreate

stop:
	docker-compose -f docker-compose.yml down

up: build run
	










