import React, { useState, useContext, useEffect } from 'react'
import { useParams, useHistory } from 'react-router';
import { AlertaDispatchContext } from '../context/alertContext';
import { API_ACCOUNT_TRANFER, API_ACCOUNT } from '../constants';
import { IErrorMessage } from '../models/ErrorMessage';
import { Spinner, Badge, Alert } from 'reactstrap';
import { IAccountModel } from '../models/Account';
import { useForm } from 'react-hook-form';
import { Link } from 'react-router-dom';
import './container.css';

export const AccountTranfer: React.FC = () => {
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
                : <AccountTranferForm account={account} />
        }
    </div>
}

interface ITranfer {
    fromAgency: string,
    fromNumber: string,
    toAgency: string,
    toNumber: string,
    transacionValue: number
}

const AccountTranferForm: React.FC<{ account: IAccountModel }> =
    ({ account }) => {
        const { register, handleSubmit, errors, } = useForm<ITranfer>(
            {
                defaultValues: {
                    fromAgency: account.agency,
                    fromNumber: account.number,
                    toAgency: "",
                    toNumber: "",
                    transacionValue: 0
                }
            });

        const AlertDispatch = useContext(AlertaDispatchContext);
        const history = useHistory();

        const onSubmit = handleSubmit(async (data) => {
            const tranfer = {
                fromAgency: account.agency,
                fromNumber: account.number,
                toAgency: data.toAgency,
                toNumber: data.toNumber,
                transacionValue: Number(data.transacionValue)
            } as ITranfer;

            const requestOptions = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(tranfer)
            };
            const response = await fetch(API_ACCOUNT_TRANFER, requestOptions);

            if (response.status === 200) {
                AlertDispatch({ typeAction: "SHOW", type: "SUCCESS", message: "Transfer completed" });

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
                <label htmlFor="fromAgency">From Agency</label>
                <input type="text" ref={register} disabled={true} name="fromAgency" id="fromAgency" className="form-control" />

            </div>
            <div className="form-group">
                <label htmlFor="fromNumber">From Account Number</label>
                <input type="text" ref={register} disabled={true} name="fromNumber" id="fromNumber" className="form-control" />
            </div>

            <div className="form-group">
                <label htmlFor="toAgency">To Agency</label>
                <input type="text" ref={register({ required: true, maxLength: 10 })} name="toAgency" id="toAgency" placeholder="To Agency" className="form-control" />
                {errors.toAgency && errors.toAgency.type == "required" && <Alert color="warning">Agency is required</Alert>}
                {errors.toAgency && errors.toAgency.type == "maxLength" && <Alert color="warning">MaxLength is 10</Alert>}
            </div>
            <div className="form-group">
                <label htmlFor="toNumber">To Account Number</label>
                <input type="text" ref={register({ required: true, maxLength: 10 })} name="toNumber" id="toNumber" placeholder="To Account Number" className="form-control" />
                {errors.toNumber && errors.toNumber.type == "required" && <Alert color="warning">Number is required</Alert>}
                {errors.toNumber && errors.toNumber.type == "maxLength" && <Alert color="warning">MaxLength is 10</Alert>}
            </div>
            <div className="form-group">
                <label htmlFor="transacionValue">Value</label>
                <input type="number" min="0" max="99999999" step=".01" ref={register({ required: true, maxLength: 10, min: 0.01, max: 99999999 })} name="transacionValue" id="transacionValue" className="form-control" />
                {errors.transacionValue && errors.transacionValue.type == "required" && <Alert color="warning">Value is required</Alert>}
                {errors.transacionValue && errors.transacionValue.type == "maxLength" && <Alert color="warning">MaxLength is 10</Alert>}
                {errors.transacionValue && errors.transacionValue.type == "min" && <Alert color="warning">Minimum value is 0.01</Alert>}
                {errors.transacionValue && errors.transacionValue.type == "max" && <Alert color="warning">Maximum value is 99999999</Alert>}
            </div>
            <button className="btn btn-primary">Confirm</button>{' '}
            <Link className="btn btn-danger" to="/account">Cancel</Link>
        </form>
    }
