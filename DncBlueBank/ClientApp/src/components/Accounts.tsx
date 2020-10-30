import React, { useState, useEffect } from 'react';
import { IAccountModel } from '../models/Account';

export const Accounts: React.FC = () => {
    const [accounts, setAccounts] = useState<Array<IAccountModel>>([] as Array<IAccountModel>);
    const [loading, setLoading] = useState<boolean>(true);


    const renderAccountTable = () =>
        (<table className='table table-striped' aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Agency</th>
                    <th>Account</th>
                    <th>Onwer</th>
                    <th>Ballance</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {accounts.map(ac =>
                    <tr key={ac.agency.toString().concat(ac.number.toString())}>
                        <td>{ac.agency}</td>
                        <td>{ac.number}</td>
                        <td>{ac.owner}</td>
                        <td>{Number(ac.balance).toLocaleString('en-US', {style: 'currency', currency: 'USD' })}</td>
                        <td><button type="button" className="btn btn-secondary">History</button></td>
                    </tr>
                )}
            </tbody>
        </table>);

    const fetchData = async () => {
        const response = await fetch('account');
        const data = await response.json();

        setAccounts(data as Array<IAccountModel>);
        setLoading(false);
    }

    useEffect(() => {
        fetchData()
    }, []);

    return <div>
        <h1>Accounts</h1>
        {
            loading ? 'Loading' : renderAccountTable()
        }
    </div>
}