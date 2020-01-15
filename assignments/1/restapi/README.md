# REST API example

This is the REST API example project from the first night of class. The following explain
a few useful things in case you want to get stuff running.

## Tooling you need

* [Current docker command line stuff](https://docs.docker.com/install/)
* [Current dotnet core](https://github.com/dotnet/core/releases/tag/v2.0.6)
* Your favorite editor

## To build the application

    dotnet publish

## To build and run the application locally

    dotnet restore
    dotnet build
    dotnet run    

## Building and pushing the container (for me)

    docker login

    docker build . -t mjmiller/su_segr:restapi
    docker push mjmiller/su_segr:restapi

## To run an existing container (for you)

    docker run -p 0.0.0.0:5000:5000 mjmiller/su_segr:restapi
