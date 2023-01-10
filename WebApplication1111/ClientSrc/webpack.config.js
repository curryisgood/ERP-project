const path = require('path');

module.exports = {
  mode: 'none',
  entry: {
    sign: './sign.js',
    signin: './signin.js',
    productInput: './productInput.js',
    productGet: './productGet.js',
    accountPost: './accountPost.js',
    accountGet: './accountGet.js',
    purchaseInput: '/purchaseInput.js',
    purchaseList: '/purchaseList.js',
    saleInput: './saleInput.js',
    saleList: './saleList.js',
  },

  output: {
    path: path.join(__dirname, '../Scripts'),
  },
};
