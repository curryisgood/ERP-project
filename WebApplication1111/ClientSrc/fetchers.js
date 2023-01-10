export const signup = (id, password) => {
  const sininInfo = {
    ID: id,
    password: password,
  };
  console.log('@@@@ ', id, password);
  fetch('/signup', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(sininInfo),
  });
};
