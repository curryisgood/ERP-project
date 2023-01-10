/******/ (() => { // webpackBootstrap
var __webpack_exports__ = {};
const formEl = document.querySelector('#prodInput');
const nameEl = document.querySelector('#prodName');
const typeEl = document.querySelector(
  'input[name="prodCategory"]:checked'
).value;
const StockEl = document.querySelector('#prodSafeCnt');
const prodInputBtn = document.querySelector('#prodInputBtn');

document.addEventListener('DOMContentLoaded', () => {
  productInput();
});

const productInput = () => {
  prodInputBtn.addEventListener('click', async (e) => {
    e.preventDefault();

    // 폼 빈칸 확인 +
    if (!(nameEl.value && typeEl && StockEl.value)) {
      alert('입력한 내역에 빈 칸이 있습니다');
      return;
    }
    console.log(nameEl.value, typeEl, StockEl.value);

    const res = await fetch('/Product/', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        Code: '',
        Name: nameEl.value,
        Type: typeEl,
        CntStock: Number(StockEl.value),
        UserID: '',
      }),
    });
    const data = await res.json();

    if (data.err) {
      alert(data.err);
    }
    if (data.message) {
      alert(data.message);
      window.location.href = '/product';
    }
  });
};

/******/ })()
;