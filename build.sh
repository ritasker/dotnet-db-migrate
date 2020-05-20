#!/usr/bin/env bash
set -e

docker build -f build.dockerfile -t db-tools-build .

epoch=$(date +%s)

docker run --rm --name db-tools-build-${epoch} 
 -v /var/run/docker.sock:/var/run/docker.sock \
 -v $PWD/artifacts:/repo/artifacts \
 --network host \
 db-tools-build