class ResultCard extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="panel panel-default">
                <div className="panel-body">
                    <h2>{this.props.result.result}</h2>
                    <p>{this.props.result.username}</p>
                </div>
            </div>
            );
    }
}