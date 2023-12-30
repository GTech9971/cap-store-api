FROM postgres:latest

# 初期化スクリプトをコピー
COPY *.csv /tmp/init_data/
COPY init.sql /docker-entrypoint-initdb.d/