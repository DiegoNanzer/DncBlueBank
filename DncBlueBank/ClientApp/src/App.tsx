import React, { Component } from 'react';
import './custom.css'
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Accounts } from './components/Accounts';
import { AlerContextProvider } from './context/alertContext';
import { MyAlert } from './components/Alert';
import { AccountCreate } from './components/AccountCreate';
import { AccountUpdate } from './components/AccountUpdate';
import { AccountTransaction } from './components/AccountTransaction';
import { AccountDeposit } from './components/AccountDeposit';
import { AccountWithDraw } from './components/AccountWithDraw';
import { AccountTranfer } from './components/AccountTranfer';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <AlerContextProvider>
                <Layout>
                    <Route exact path='/' component={Accounts} />
                    <Route exact path='/account' component={Accounts} />
                    <Route exact path='/account/create' component={AccountCreate} />
                    <Route exact path='/account/update/:id' component={AccountUpdate} />
                    <Route exact path='/account/transaction/:id' component={AccountTransaction} />
                    <Route exact path='/account/deposit/:id' component={AccountDeposit} />
                    <Route exact path='/account/withdraw/:id' component={AccountWithDraw} />
                    <Route exact path='/account/tranfer/:id' component={AccountTranfer} />
                </Layout>
                <MyAlert />
            </AlerContextProvider>
        );
    }
}
