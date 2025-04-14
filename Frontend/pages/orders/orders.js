import { get, post } from '../../lib/helpers/httpClient.js';

const form = document.querySelector('#order-form');
const nameInput = document.querySelector('#order-name');
const productSelect = document.querySelector('#order-product');
const quantityInput = document.querySelector('#order-quantity');
const messageBox = document.querySelector('#order-message');

const initApp = () => {
    loadProducts();
    form.addEventListener('submit', handleSubmit);
};

const loadProducts = async () => {
    const response = await get('products');
    const products = response.data;

    products.forEach(product => {
        const option = document.createElement('option');
        option.value = product.itemNumber;
        option.textContent = product.name;
        productSelect.appendChild(option);
    });
};

const handleSubmit = async (e) => {
    e.preventDefault();

    const order = {
        customerName: nameInput.value,
        productId: parseInt(productSelect.value),
        quantity: parseInt(quantityInput.value)
    };

    try {
        await post('order', order);
        messageBox.textContent = '✅ Beställning skickad!';
        form.reset();
    } catch {
        messageBox.textContent = 'Något gick fel.';
    }
};

document.addEventListener('DOMContentLoaded', initApp);