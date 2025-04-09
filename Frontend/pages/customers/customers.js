import { get } from '../../lib/helpers/httpClient.js';

const customerList = document.querySelector('#customers-list');

const initApp = () => {
  loadCustomers();
};

const loadCustomers = async () => {
  try {
    const response = await get('customer');
    const customers = response.data;

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

document.addEventListener('DOMContentLoaded', initApp);