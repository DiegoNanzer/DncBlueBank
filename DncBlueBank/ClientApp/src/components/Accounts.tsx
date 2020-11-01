import React, { useState, useEffect, useContext } from 'react';
import { IAccountModel } from '../models/Account';
import { API_ACCOUNT } from '../constants';
import { AlertaDispatchContext } from '../context/alertContext';
import { IErrorMessage } from '../models/ErrorMessage';
import { Spinner } from 'reactstrap';
import { Link } from 'react-router-dom';

export const Accounts: React.FC = () => {
    const [accounts, setAccounts] = useState<Array<IAccountModel>>([] as Array<IAccountModel>);
    const [loading, setLoading] = useState<boolean>(true);
    const AlertDispatch = useContext(AlertaDispatchContext);


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
                    <tr key={ac.id}>
                        <td>{ac.agency}</td>
                        <td>{ac.number}</td>
                        <td>{ac.owner}</td>
                        <td>{Number(ac.balance).toLocaleString('en-US', { style: 'currency', currency: 'USD' })}</td>
                        <td><Link className="btn btn-secondary" to={`/account/transaction/${ac.id}`}>Transacions</Link>{' '}
                            <Link className="btn btn-primary" to={`/account/update/${ac.id}`}>Edit</Link>{' '}
                            <Link className="btn btn-success" to={`/account/deposit/${ac.id}`}>Deposit</Link>{' '}
                            <Link className="btn btn-danger" to={`/account/withdraw/${ac.id}`}>Withdraw</Link>{' '}
                            <Link className="btn btn-dark" to={`/account/tranfer/${ac.id}`}>Transfer</Link>{' '}
                        </td>
                    </tr>
                )}
            </tbody>
        </table>);


    const fetchData = async () => {
        try {
            const response = await fetch(API_ACCOUNT);

            if (response.status === 200) {
                const data = await response.json();
                setAccounts(data as Array<IAccountModel>);
            }
            else if (response.status === 400) {
                const data = await response.json();

                AlertDispatch({ typeAction: "SHOW", type: "ERROR", message: (data as IErrorMessage).message });
            }
            else {
                console.log(response);
            }

            setLoading(false);

        } catch (e) {
            console.log(e);
        }

    }

    useEffect(() => { fetchData() }
        , []);

    return <div>
        <h1>Accounts</h1>
        {
            loading ? <Spinner color="dark" />
                : <React.Fragment>
                    {renderAccountTable()}
                    <Link className="btn btn-primary" to="/account/create">New Account</Link>
                </React.Fragment>
        }

    </div>
}