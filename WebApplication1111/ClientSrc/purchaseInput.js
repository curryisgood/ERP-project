const purchaseForm = document.querySelector('#purchase');
const productEl = document.querySelector('#products');
const quantityEl = document.querySelector('#quantity');
const customersEl = document.querySelector('#customers');

document.addEventListener('DOMContentLoaded', () => {
  registSubmitPurchaseEvent();
});

const registSubmitPurchaseEvent = () => {
  purchaseForm.addEventListener('submit', async (e) => {
    e.preventDefault();

    if (!(productEl.value && quantityEl.value && customersEl.value)) {
      alert('내용을 입력해주세요');
      return;
    }

    const res = await fetch('/Purchase/', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        Product: productEl.value,
        Count: quantityEl.value,
        UserID: '',
        Code: '',
        Account: customersEl.value,
      }),
    });
    const data = await res.json();
    console.log(data);
    alert(data.message);
  });
};
