#!/bin/sh
set -e

mongoimport \
  --host localhost \
  --db PeoplePersonalities \
  --collection PeoplePersonalitiesMocks \
  --jsonArray \
  --file /seed/MockData.json

# Uncomment this section for production purposes
#mongoimport \
#  --host localhost \ 
#  --db PeoplePersonalities \
#  --collection PeoplePersonalities