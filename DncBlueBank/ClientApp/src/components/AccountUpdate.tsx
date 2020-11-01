import React, { useContext, useEffect, useState } from 'react';
import { Button, Form, FormGroup, Label, Input, FormText, Alert, Spinner } from 'reactstrap';
import { Link, Redirect, useHistory, useParams } from 'react-router-dom';
import { useForm } from "react-hook-form";
import { IAccountModel } from '../models/Account';
import { API_ACCOUNT } from '../constants';
import { AlertaDispatchContext } from '../context/alertContext';
import { IErrorMessage } from '../models/ErrorMessage';
import './container.css';

export const AccountUpdate: React.FC = () => {
    const [loading, setLoading] = useState<boolean>(true);
    const [accountDefault, setAccountDefault] = useState<IAccountModel>({} as IAccountModel);

    const { id } = useParams();
    const AlertDispatch = useContext(AlertaDispatchContext);

    useEffect(() => { fetchAccount(id) }
        , []);

    const fetchAccount = async (id: string) => {
        try {
            const response = await fetch(`${API_ACCOUNT}/${id}`);

            if (response.status === 200) {
                const data = await response.json();
                setAccountDefault(data as IAccountModel);
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

    return <div className="container myContainer">
        <h1>Account Edit Form</ h1>
        {loading ? <Spinner color="dark" />
            : <AccountUpdateForm account={accountDefault} />
        }
    </div>
}

const AccountUpdateForm: React.FC<{ account: IAccountModel }> = ({ account }) => {
    const { register, handleSubmit, errors, } = useForm({ defaultValues: account });
    const AlertDispatch = useContext(AlertaDispatchContext);
    const history = useHistory();

    const onSubmit = handleSubmit(async (data) => {
        var accountUpdate = { ...account, owner: data.owner };

        const requestOptions = {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(accountUpdate)
        };

        const response = await fetch(API_ACCOUNT, requestOptions);

        if (response.status === 200) {
            AlertDispatch({ typeAction: "SHOW", type: "SUCCESS", message: "Account updated" });

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
            <label htmlFor="owner">Owner</label>
            <input type="text" ref={register({ required: true, maxLength: 10 })} name="owner" id="owner" className="form-control" />
            {errors.owner && errors.owner.type == "required" && <Alert color="warning">Owner is required</Alert>}
            {errors.owner && errors.owner.type == "maxLength" && <Alert color="warning">MaxLength is 10</Alert>}
        </div>
        <button className="btn btn-primary">Save</button>{' '}
        <Link className="btn btn-danger" to="/account">Cancel</Link>
    </form>
}