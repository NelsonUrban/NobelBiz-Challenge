const API_ENDPOINT = "https://api.sampleapis.com/codingresources/codingResources";
const FORM_DATA_KEY = "formData";

const customerForm = {};
let data = [];
let dataToShown = [];

const codingResourcesService = async () => {
    try {
        return await (await fetch(API_ENDPOINT)).json();
    }
    catch {
        console.error(`Service ${API_ENDPOINT} not available`)
    }
}

const getDataFromEndpoint = async () => {
    data = await codingResourcesService();
    dataToShown = data.splice(0, 9);

    loadTableData(dataToShown);
}

const loadTableData = (dataToBeAdded) => {
    const table = document.querySelector("table > tbody");

    for (let item of dataToBeAdded) {
        table.insertAdjacentHTML("beforeend", `
            <tr>
                <td>
                    <a href="${item.url}">${item.description}</a>
                </td>
                <td>${item.types.join(" ")}</td>
                <td>${item.topics.join(", ")}</td>
            </tr>
        `);
    }
}

const loadNext10 = () => {
    dataToShown = [...dataToShown, data.splice(0, 9)];
    loadTableData(dataToShown);
}

const loadCustomerData = () => {
    const data = JSON.parse(localStorage.getItem(FORM_DATA_KEY));

    if (!data) return;

    document.querySelectorAll("input").forEach(input => {
        const inputName = input.getAttribute("name");

        if (data[inputName]) {
            input.value = data[inputName];
            customerForm[inputName] = data[inputName]
        }
    });

    updateCustomerName();
};

const updateCustomerName = () => {
    if (customerForm.firstname || customerForm.lastname) {
        document.querySelectorAll(".spCustomer").forEach(span => {
            span.innerText = `${customerForm.firstname} ${customerForm.lastname}`;
        });
    }
}

const setUpCustomerForm = () => {
    const saveInputForm = (event) => {
        const formItem = event.target.getAttribute("name"); 
        customerForm[formItem] = event.target.value;
        localStorage.setItem(FORM_DATA_KEY, JSON.stringify(customerForm));
        updateCustomerName();
    }

    document.querySelectorAll("input").forEach(input => {
        input.addEventListener("input", saveInputForm);
    });
}

document.addEventListener("DOMContentLoaded", function() {
    loadCustomerData();
    getDataFromEndpoint();
    setUpCustomerForm();

    document.querySelector("button").addEventListener("click", loadNext10);

    setInterval(() => {
        console.log(customerForm);
    }, 3000)
});
