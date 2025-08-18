FROM postgres:16-alpine

# Copy all SQL initialization scripts into the standard Postgres init folder
COPY init/ /docker-entrypoint-initdb.d/