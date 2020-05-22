#!/bin/bash
set -e

docker build -t crashtest:latest .

docker run -t crashtest:latest .
