const productContainerEl = document.querySelector('#productContainer');

document.addEventListener('DOMContentLoaded', () => {
  registButtonEvent();
});

const registButtonEvent = () => {
  productContainerEl.addEventListener('click', (e) => {
    if (e.target.dataset.btn == 'edit') updateProd(e);
    if (e.target.dataset.btn == 'delete') deleteProduct(e);
  });
};

const updateProd = (element) => {
  const siblingsElements = getSiblingsValue(element);
  siblingsElements.editFormEl.hidden = !siblingsElements.editFormEl.hidden;
  siblingsElements.editFormEl.onsubmit = (e) => {
    e.preventDefault();
    handleEditProduct(siblingsElements);
  };
};

const handleEditProduct = async (siblingsElements) => {
  const { editFormEl, code } = siblingsElements;
  const newName = editFormEl.querySelector('#productName');
  const newType = editFormEl.querySelector('#productType');
  const newSaftyStock = editFormEl.querySelector('#productSaftyStock');

  try {
    // ================= 업데이트
    const res = await fetch('/product/updateProduct', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        Code: code,
        Name: newName.value,
        Type: newType.value,
        CntStock: Number(newSaftyStock.value),
        UserID: '',
      }),
    });

    if (res.status === 400) {
      throw new Error('이미 존재하는 품목입니다.');
    }

    const data = await res.json();
    alert(data.message);
    window.location.href = '/product/prodLst';
  } catch (err) {
    alert(err);
  }
};

// ============== 삭제
const deleteProduct = async (element) => {
  const { code, name, category, safetyStock } = getSiblingsValue(element);

  const res = await fetch('/Product/deleteProduct', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      Code: code,
      Name: name,
      Type: category,
      CntStock: Number(safetyStock),
      userID: '',
    }),
  });
  const data = await res.json();
  alert(data.message);
  window.location.href = '/product/prodLst';
};

const getSiblingsValue = (element) => {
  const parentNode = element.target.parentNode;
  const siblingsValue = {
    code: parentNode.querySelector('#code').textContent,
    name: parentNode.querySelector('#name').textContent,
    category: parentNode.querySelector('#category').textContent,
    safetyStock: parentNode.querySelector('#safetyStock').textContent,
    editFormEl: parentNode.querySelector('#editForm'),
  };
  return siblingsValue;
};
