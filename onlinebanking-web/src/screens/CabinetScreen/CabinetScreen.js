import React from 'react';
import CabinetLayout from '../../layouts/CabinetLayout/CabinetLayout';
import CreditCardsContainer from '../../containers/CreditCardsContainer/CreditCardsContainer';
import AccountProvider from '../../providers/AccountProvider';
import OrderCreditCardContainer from '../../containers/OrderCreditCardContainer/OrderCreditCardContainer';

import { Switch, Route } from 'react-router-dom';

const userRoles = {
    admin: 'admin',
    manager: 'manager',
    user: 'user'
};

class CabinetScreen extends React.Component {

    render() {
        const { match } = this.props;

        return (
            <CabinetLayout role={match.params.role}>
                <AccountProvider>
                    <Switch>
                        <Route path={`/cab/${userRoles.user}/credit-cards/list`} component={CreditCardsContainer} />
                        <Route path={`/cab/${userRoles.user}/credit-cards/order`} component={OrderCreditCardContainer} />
                    </Switch>
                </AccountProvider>
            </CabinetLayout>
        );
    }
}

export default CabinetScreen;