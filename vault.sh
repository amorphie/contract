sleep 5 &&
curl -X POST 'http://vault:8200/v1/secret/data/vb1' -H "Content-Type: application/json" -H "X-Vault-Token: admin" -d '{ "data": {"pass": "my-password", "username":"my-username"} }'
