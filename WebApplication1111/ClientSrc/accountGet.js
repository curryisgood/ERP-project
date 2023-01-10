const customerContainerEl = document.querySelector('#customerContainer');

document.addEventListener('DOMContentLoaded', () => {
  registButtonEvent();
});

const registButtonEvent = () => {
  customerContainerEl.addEventListener('click', (e) => {
    if (e.target.dataset.btn == 'edit') editCustomer(e);
    if (e.target.dataset.btn == 'delete') deleteCustomer(e);
  });
};

const editCustomer = (element) => {
  const siblingsElements = getSiblingsValue(element);
  siblingsElements.editFormEl.hidden = !siblingsElements.editFormEl.hidden;
  siblingsElements.editFormEl.onsubmit = (e) => {
    e.preventDefault();
    handleEditCustomer(siblingsElements);
  };
};

const handleEditCustomer = async (siblingsElements) => {
  const { editFormEl, Code } = siblingsElements;
  const name = editFormEl.querySelector('#customerName');
  try {
    // 업데이트= =========================
    const res = await fetch('/account/updateCustomer', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        Code: Code,
        Name: name.value,
        UserID: '',
      }),
    });

    if (res.status === 400) {
      throw new Error('이미 존재하는 거래처입니다.');
    }

    // const data = await res.json();
    // alert(data.message);
    window.location.href = '/account/acntLst';
  } catch (err) {
    alert(err);
  }
};

// 삭제 ==============
const deleteCustomer = async (element) => {
  const { Code, Name } = getSiblingsValue(element);
  console.log(Code, Name);
  const res = await fetch('/account/deleteCustomer', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      Code: Code,
      Name: Name,
      UserID: '',
    }),
  });

  // const data = await res.json();

  // alert(data.message);
  // alert('삭제동작');
  window.location.href = '/account/acntLst';
};

const getSiblingsValue = (element) => {
  const parentNode = element.target.parentNode;
  const siblingsValue = {
    Code: parentNode.querySelector('#code').textContent,
    Name: parentNode.querySelector('#name').textContent,
    editFormEl: parentNode.querySelector('#editForm'),
  };
  return siblingsValue;
};
