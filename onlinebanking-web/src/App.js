import React from 'react';
import HomeScreen from './screens/Home/Home';
import CabinetScreen from './screens/CabinetScreen/CabinetScreen';

import {
  BrowserRouter as Router,
  Route,
  Switch
} from 'react-router-dom';

export default () => (
  <Router>
    <Switch>
      <Route exact path="/" component={HomeScreen} />
      <Route path="/cab/:role" component={CabinetScreen} />
    </Switch>
  </Router>
)
