const idEl = document.querySelector('#idInput');
const passwordEl = document.querySelector('#pwdInput');
const loginFormEl = document.querySelector('#login');

document.addEventListener('DOMContentLoaded', () => {
  registLoginEvent();
});

const registLoginEvent = () => {
  loginFormEl.addEventListener('submit', async (e) => {
    e.preventDefault();

    const res = await fetch('/', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ ID: idEl.value, Password: passwordEl.value }),
    });
    console.log('signin');
    const data = await res.json();

    alert(data.message);
    window.location.href = '/';
  });
};
