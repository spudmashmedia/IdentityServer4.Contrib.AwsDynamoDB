#!/bin/bash
set -e
docker build -t id4tools .
docker run -it -v ~/.aws:/root/.aws id4tools bash