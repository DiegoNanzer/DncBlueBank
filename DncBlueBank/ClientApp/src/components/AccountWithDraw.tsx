import React, { useState, useContext, useEffect } from 'react'
import { useParams, useHistory } from 'react-router';
import { AlertaDispatchContext } from '../context/alertContext';
import { API_ACCOUNT_WITHDRAW, API_ACCOUNT } from '../constants';
import { IErrorMessage } from '../models/ErrorMessage';
import { Spinner, Badge, Alert } from 'reactstrap';
import { IAccountModel } from '../models/Account';
import { useForm } from 'react-hook-form';
import { Link } from 'react-router-dom';
import './container.css';

export const AccountWithDraw: React.FC = () => {
    const [loading, setLoading] = useState<boolean>(true);
    const { id } = useParams();
    const [account, setAccount] = useState<IAccountModel>({} as IAccountModel);
    const AlertDispatch = useContext(AlertaDispatchContext);

    const fetchAccount = async (id: string) => {
        try {
            const response = await fetch(`${API_ACCOUNT}/${id}`);

            if (response.status === 200) {
                const data = await response.json();
                setAccount(data as IAccountModel);
            }
            else if (response.status === 404) {
                console.log(response);
                AlertDispatch({ typeAction: "SHOW", type: "ERROR", message: "Not found" });
            }
            else {
                console.log(response);
            }

            setLoading(false);

        } catch (e) {
            console.log(e);
        }
    }
    useEffect(() => { fetchAccount(id) }
        , []);

    return <div className="container myContainer">
        <h1>Withdraw</h1>
        {
            loading ? <Spinner color="dark" />
                : <AccountWithDrawForm account={account} />
        }
    </div>
}

interface IWithdraw {
    agency: string,
    number: string,
    transacionValue: number
}

const AccountWithDrawForm: React.FC<{ account: IAccountModel }> =
    ({ account }) => {
        const { register, handleSubmit, errors, } = useForm<IWithdraw>(
            { defaultValues: { agency: account.agency, number: account.number, transacionValue: 0 } });
        const AlertDispatch = useContext(AlertaDispatchContext);
        const history = useHistory();

        const onSubmit = handleSubmit(async (data) => {
            const withdraw = {
                agency: account.agency,
                number: account.number,
                transacionValue: Number(data.transacionValue)
            } as IWithdraw;
            const requestOptions = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(withdraw)
            };
            const response = await fetch(API_ACCOUNT_WITHDRAW, requestOptions);

            if (response.status === 200) {
                AlertDispatch({ typeAction: "SHOW", type: "SUCCESS", message: "Deposit completed" });

                setTimeout(function () {
                    AlertDispatch({ typeAction: "HIDDEN" });
                    history.push("/account");
                }, 1500)

            }
            else if (response.status === 400) {
                const data = await response.json();

                AlertDispatch({ typeAction: "SHOW", type: "ERROR", message: (data as IErrorMessage).message });
            }
            else {
                console.log(response);
            }
        });


        return <form onSubmit={onSubmit}>
            <div className="form-group">
                <label htmlFor="agency">Agency</label>
                <input type="text" ref={register} disabled={true} name="agency" id="agency" className="form-control" />

            </div>
            <div className="form-group">
                <label htmlFor="number">Account Number</label>
                <input type="text" ref={register} disabled={true} name="number" id="number" className="form-control" />
            </div>
            <div className="form-group">
                <label htmlFor="transacionValue">Value</label>
                <input type="number" min="0" max="99999999" step=".01" ref={register({ required: true, maxLength: 10, min: 0.01, max: 99999999 })}
                    name="transacionValue" id="transacionValue" className="form-control" />
                {errors.transacionValue && errors.transacionValue.type == "required" && <Alert color="warning">Value is required</Alert>}
                {errors.transacionValue && errors.transacionValue.type == "maxLength" && <Alert color="warning">MaxLength is 10</Alert>}
                {errors.transacionValue && errors.transacionValue.type == "min" && <Alert color="warning">Minimum value is 0.01</Alert>}
                {errors.transacionValue && errors.transacionValue.type == "max" && <Alert color="warning">Maximum value is 99999999</Alert>}
            </div>
            <button className="btn btn-primary">Confirm</button>{' '}
            <Link className="btn btn-danger" to="/account">Cancel</Link>
        </form>
    }
