/******/ (() => { // webpackBootstrap
var __webpack_exports__ = {};
const saleContainerEl = document.querySelector('#saleContainer');

document.addEventListener('DOMContentLoaded', () => {
  registButtonEvent();
});

const registButtonEvent = () => {
  saleContainerEl.addEventListener('click', (e) => {
    if (e.target.dataset.btn == 'edit') editSale(e);
    if (e.target.dataset.btn == 'delete') deleteSale(e);
  });
};

const editSale = (element) => {
  const siblings = getSiblingsValue(element);
  siblings.editFormEl.hidden = !siblings.editFormEl.hidden;

  siblings.editFormEl.onsubmit = (e) => {
    e.preventDefault();
    handleEditPurchase(siblings);
  };
};

const handleEditPurchase = async (siblings) => {
  const newProduct = siblings.editFormEl.querySelector('#products');
  const newQuantity = siblings.editFormEl.querySelector('#purchaseQuantity');
  const newCustomer = siblings.editFormEl.querySelector('#customers');

  // 업데이트==================
  const res = await fetch('/sale/updateSale', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      Product: newProduct.value,
      Count: newQuantity.value,
      UserID: '',
      Code: siblings.code,
      Account: newCustomer.value,
    }),
  });

  const data = await res.json();
  alert(data.message);
  window.location.href = '/sale/SaleList';
};

// 삭제===============
const deleteSale = async (element) => {
  const { code } = getSiblingsValue(element);

  const res = await fetch('/sale/deleteSale', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ Code: code }),
  });

  const data = await res.json();

  alert(data.message);
  window.location.href = '/sale/SaleList';
};

const getSiblingsValue = (element) => {
  const parentNode = element.target.parentNode;
  const siblingsValue = {
    code: parentNode.querySelector('#code').textContent,
    name: parentNode.querySelector('#name').textContent,
    quantity: parentNode.querySelector('#quantity').textContent,
    editFormEl: parentNode.querySelector('#editForm'),
  };
  return siblingsValue;
};

/******/ })()
;