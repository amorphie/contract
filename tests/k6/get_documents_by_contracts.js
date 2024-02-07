import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
  vus: 5,
  maxVUs: 2000,
  stages: [
    { duration: '2m', target: 1000 }, // 2 dakika boyunca 1000 request atılacak
    { duration: '1m', target: 2000 }, // Sonraki 1 dakikada 2000 request atılacak
  ],
};

export default function () {

  const randomNumber = Math.floor(Math.random() * 50);
  const randomReference = Math.floor(Math.random() * (100000 - 10000 + 1) + 10000);


  let url = `https://test-amorphie-contract.burgan.com.tr/customer/get-documents-by-contracts?Code=test-hesap-acilis&Page=0&PageSize=${randomNumber}&Reference=${randomReference}`;

  let response = http.get(url);
  check(response, {

  });

  sleep(Math.random() * 3);
}