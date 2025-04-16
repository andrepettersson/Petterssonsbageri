import * as http from "../../lib/helpers/httpClient.js";

const form = document.querySelector('#supplier-form');
const nameInput = document.querySelector('#supplier-name');
const contactInput = document.querySelector('#supplier-contact');
const emailInput = document.querySelector('#supplier-email');
const phoneInput = document.querySelector('#supplier-phone');
const supplierList = document.querySelector('#supplier-list');

const initApp = () => {
    loadSuppliers();
    form.addEventListener('submit', handleSubmit);
};

const loadSuppliers = async () => {
    try {
        const response = await http.get('suppliers');
        const suppliers = response.suppliers;

        supplierList.innerHTML = ''; 

        for (let supplier of suppliers) {
            generateSupplierHtml(supplier);
        }
    } catch (error) {
        console.error('Kunde inte hämta leverantörer:', error);
    }
};

const generateSupplierHtml = (supplier) => {
    const section = document.createElement('section');
    section.classList.add('card');

    const company = document.createElement('p');
    company.innerText = `Namn: ${supplier.firstName} ${supplier.lastName}`;

    const contact = document.createElement('p');
    contact.innerText = `Kontaktperson: ${supplier.contactPerson}`;

    const phone = document.createElement('p');
    phone.innerText = `Telefon: ${supplier.phone}`;

    const email = document.createElement('p');
    email.innerText = `E-post: ${supplier.email}`;

    section.appendChild(company);
    section.appendChild(contact);
    section.appendChild(phone);
    section.appendChild(email);

    supplierList.appendChild(section);
};

const handleSubmit = async (e) => {
    e.preventDefault();

    const newSupplier = {
        companyName: nameInput.value,
        contactPerson: contactInput.value,
        email: emailInput.value,
        phone: phoneInput.value
    };

    try {
        await http.post('suppliers', newSupplier);
        form.reset();
        loadSuppliers();
    } catch (error) {
        console.error('Kunde inte lägga till leverantör:', error);
    }
};

document.addEventListener('DOMContentLoaded', initApp);