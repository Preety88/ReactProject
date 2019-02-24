import React, { Component } from 'react';
import ReactDOM from 'react-dom';

import CustomersTable from './Components/Customer/CustomersTable';


const app = document.getElementById('customers');
ReactDOM.render(<CustomersTable />, app);

