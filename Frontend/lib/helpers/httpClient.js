import { config } from './config.js';

export const get = async (endpoint) => {
  const url = `${config.apiUrl}/${endpoint}`;
  const response = await fetch(url);

  if (!response.ok) throw new Error("Kunde inte hämta listan");
  return await response.json();
};


export const post = async (endpoint, data) => {
  const url = `${config.apiUrl}/${endpoint}`;
  const response = await fetch(url, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      "x-apikey": config.apiKey
    },
    body: JSON.stringify(data)
  });

  if (!response.ok) throw new Error("Kunde inte lägga till i listan");
  return await response.json();


};

export const put = async (endpoint, id, data) => {
  const url = `${config.apiUrl}/${endpoint}/${id}`;
  const response = await fetch(url, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      "x-apikey": config.apiKey
    },
    body: JSON.stringify(data)
  });

  if (!response.ok) throw new Error("Kunde inte uppdatera listan");
  return await response.json();
};
