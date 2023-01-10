/******/ (() => { // webpackBootstrap
var __webpack_exports__ = {};
const idEl = document.querySelector('#idInput');
const passwordEl = document.querySelector('#pwdInput');
const loginFormEl = document.querySelector('#signup');

document.addEventListener('DOMContentLoaded', () => {
  console.log('가입');
  registLoginEvent();
});

const registLoginEvent = () => {
  loginFormEl.addEventListener('submit', async (e) => {
    e.preventDefault();

    const res = await fetch('/Home/signup', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ ID: idEl.value, Password: passwordEl.value }),
    });

    const data = await res.json();
    if (data.err) {
      alert(data.err);
    }
    if (data.message) {
      alert(data.message);
      window.location.href = '/';
    }
  });
};

/******/ })()
;