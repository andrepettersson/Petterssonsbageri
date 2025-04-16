import { get, post } from '../../lib/helpers/httpClient.js';

const form = document.querySelector('#order-form');
const nameInput = document.querySelector('#order-name');
const contactInput = document.querySelector('#order-contact');
const emailInput = document.querySelector('#order-email');
const phoneInput = document.querySelector('#order-phone');
const productSelect = document.querySelector('#order-product');
const quantityInput = document.querySelector('#order-quantity');

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
        option.textContent = `${product.name} ${product.price} kr`;
        option.dataset.price = product.price;
        productSelect.appendChild(option);
    });
};

const handleSubmit = async (e) => {
    e.preventDefault();

    const name = nameInput.value;
    const contact = contactInput.value;
    const email = emailInput.value;
    const phone = phoneInput.value;
    const productId = parseInt(productSelect.value);
    const quantity = parseInt(quantityInput.value);
    const selectedOption = productSelect.options[productSelect.selectedIndex];
    const price = parseFloat(selectedOption.dataset.price);

    try {

        const customerResponse = await post('customer', {
            companyName: name,
            contactPerson: contact,
            email: email,
            phone: phone
        });
        const customerId = customerResponse.data.id;


        const order = {
            customerId,
            orderItems: [
                {
                    productId,
                    quantity,
                    price
                }
            ]
        };

        await post('order', order);
        form.reset();
    } catch (error) {
        console.error('Fel vid beställning:', error);
    }
};

document.addEventListener('DOMContentLoaded', initApp);