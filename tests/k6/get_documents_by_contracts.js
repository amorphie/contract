import { check, group, sleep } from 'k6';
import http from 'k6/http';

export let options = {
  vus: 10,
  stages: [
    { duration: "30s", target: 10 },
    { duration: "5m", target: 20 },
  ],
  thresholds: {
    "http_req_duration": ["avg<500"]
  }
}

const BASE_URL = "https://test-amorphie-contract.burgan.com.tr";
const LanguageCode = ["en-EN", "tr-TR"];

export default function () {
  group('Contract API testing', function () {
    group('customer', function () {

      const randomNumber = Math.floor(Math.random() * 50);
      const randomReference = Math.floor(Math.random() * (100000 - 10000 + 1) + 10000);
      const randomLangCode = Math.floor(Math.random() * 2);

      let url = `${BASE_URL}/customer/get-documents-by-contracts?Code=test-hesap-acilis&Page=0&PageSize=${randomNumber}&Reference=${randomReference}`;

      const requestHeaders = {
        'User-Agent': 'k6-serhat',
        'Accept-Language': LanguageCode[randomLangCode],
      };

      let res = http.get(url, {
        headers: requestHeaders
      });

      check(res, { "status is 200": (r) => r.status === 200 });
    });

  });
  sleep(1);
}