import { get, post } from '../../lib/helpers/httpClient.js';

const customerList = document.querySelector('#customers-list');
const form = document.querySelector('#customer-form');
const nameInput = document.querySelector('#customer-name');
const contactInput = document.querySelector('#customer-contact');
const emailInput = document.querySelector('#customer-email');
const phoneInput = document.querySelector('#customer-phone');

const initApp = () => {
  loadCustomers();
  form.addEventListener('submit', handleSubmit);
};

const loadCustomers = async () => {
  try {
    const response = await get('customer');
    const customers = response.data;

    customerList.innerHTML = '';
    for (let customer of customers) {
      generateCustomerHtml(customer);
    }
  } catch (error) {
    console.error('Kunde inte hämta kunder:', error);
  }
};

const generateCustomerHtml = (customer) => {
  const section = document.createElement('section');
  section.classList.add('card');

  const name = document.createElement('h3');
  name.innerText = customer.companyName;

  const contact = document.createElement('p');
  contact.innerText = `Kontaktperson: ${customer.contactPerson}`;

  const email = document.createElement('p');
  email.innerText = `E-post: ${customer.email}`;

  const phone = document.createElement('p');
  phone.innerText = `Telefon: ${customer.phone}`;

  section.appendChild(name);
  section.appendChild(contact);
  section.appendChild(email);
  section.appendChild(phone);

  customerList.appendChild(section);
};

const handleSubmit = async (e) => {
  e.preventDefault();

  const newCustomer = {
    companyName: nameInput.value,
    contactPerson: contactInput.value,
    email: emailInput.value,
    phone: phoneInput.value
  };

  try {
    await post('customer', newCustomer);
    form.reset();
    loadCustomers();
  } catch (error) {
    console.error('Kunde inte lägga till kund:', error);
  }
};

document.addEventListener('DOMContentLoaded', initApp);