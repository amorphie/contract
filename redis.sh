redis-server --daemonize yes && sleep 3
redis-cli MSET config-amorphie-contract-db 'User ID=postgres;Password=postgres;Host=Localhost;Port=5432;Database=contract;'
redis-cli save 
redis-cli shutdown 
redis-server