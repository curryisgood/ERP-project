const formEl = document.querySelector('#accntInput');
const nameEl = document.querySelector('#prodName');

document.addEventListener('DOMContentLoaded', () => {
  registSubmitCustomerEvent();
});

const registSubmitCustomerEvent = () => {
  formEl.addEventListener('submit', async (e) => {
    e.preventDefault();

    if (!nameEl.value) {
      alert('빈 칸이 존재합니다');
      return;
    }

    const res = await fetch('/Account/', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ Code: '', Name: nameEl.value, UserID: '' }),
    });

    const data = await res.json();
    alert(data.message);
  });
};
