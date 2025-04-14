import { get, post } from '../../lib/helpers/httpClient.js';

const productList = document.querySelector('#products-list');

const initApp = () => {
    loadProducts();
};

const loadProducts = async () => {
    try {
        const response = await get('products');
        const products = response.data;

        for (let product of products) {
            generateProductHtml(product);
        }
    } catch (error) {
        console.error('Kunde inte hämta produkter:', error);
    }
};

const generateProductHtml = (product) => {
    const section = document.createElement('section');
    section.classList.add('card');

    const name = document.createElement('h3');
    name.innerText = product.name;

    const price = document.createElement('p');
    price.innerText = `Pris: ${product.price} kr`;

    section.appendChild(name);
    section.appendChild(price);
    productList.appendChild(section);
};

const form = document.querySelector('#product-form');
form.addEventListener('submit', async (event) => {
    event.preventDefault();

    const name = document.querySelector('#product-name').value;
    const price = document.querySelector('#product-price').value;

    const newProduct = { name, price: parseFloat(price) };

    try {
        await post('products', newProduct);
        alert('Produkten har lagts till!');
        form.reset();
        productList.innerHTML = "";
        loadProducts();
    } catch (error) {
        console.error("Kunde inte lägga till produkt:", error);
    }
});

document.addEventListener('DOMContentLoaded', initApp);