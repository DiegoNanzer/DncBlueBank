import React, { useState, useContext, useEffect } from 'react'
import { useParams } from 'react-router';
import { AlertaDispatchContext } from '../context/alertContext';
import { API_ACCOUNT_HISTORY } from '../constants';
import { IAccountTransaction, eTransaction } from '../models/AccountTransaction';
import { IErrorMessage } from '../models/ErrorMessage';
import { Spinner, Badge } from 'reactstrap';


export const AccountTransaction: React.FC = () => {
    const [loading, setLoading] = useState<boolean>(true);
    const { id } = useParams();
    const [trasactions, setTrasactions] = useState<Array<IAccountTransaction>>([] as Array<IAccountTransaction>);
    const AlertDispatch = useContext(AlertaDispatchContext);

    const fetchHistory = async (id: string) => {
        try {
            const response = await fetch(API_ACCOUNT_HISTORY.replace("{id}", id));

            if (response.status === 200) {
                const data = await response.json();
                setTrasactions(data as Array<IAccountTransaction>);
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


    useEffect(() => { fetchHistory(id) }
        , []);

    return <React.Fragment>
        <h1>Accounts Transactions</h1>
        {
            loading ? <Spinner color="dark" />
                : <AccountTransactionTable transations={trasactions} />
        }
    </React.Fragment>
}

const AccountTransactionTable: React.FC<{ transations: Array<IAccountTransaction> }> =
    ({ transations }) => {

        const typeDescrition = (type: eTransaction) => {

            switch (type) {
                case eTransaction.DEPOSIT:
                    return <Badge color="success">DEPOSIT</Badge>;
                case eTransaction.WITHDRAW:
                    return <Badge color="danger">WITHDRAW</Badge>;
            }
        }

        return (<table className='table table-striped' aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Type</th>
                    <th>Value</th>
                </tr>
            </thead>
            <tbody>
                {transations.map(trans =>
                    <tr key={trans.id}>
                        <td>{typeDescrition(trans.type)}</td>
                        <td>{Number(trans.value).toLocaleString('en-US', { style: 'currency', currency: 'USD' })}</td>
                    </tr>
                )}
            </tbody>
        </table>);
    }
