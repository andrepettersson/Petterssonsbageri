import { get } from "../../lib/helpers/httpClient.js"

const supplierList = document.querySelector('#supplier-list');

const initApp = () => {
    loadSuppliers();
};

const loadSuppliers = async () => {
    try {
        const response = await get('suppliers');
        const suppliers = response.data;

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

    const name = document.createElement('h3');
    name.innerText = supplier.name;

    const phone = document.createElement('p');
    phone.innerText = `Telefon: ${supplier.phone}`;

    const email = document.createElement('p');
    email.innerText = `E-post: ${supplier.email}`;

    section.appendChild(name);
    section.appendChild(phone);
    section.appendChild(email);

    supplierList.appendChild(section);
};

document.addEventListener('DOMContentLoaded', initApp);